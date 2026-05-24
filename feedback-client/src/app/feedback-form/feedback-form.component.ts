import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FeedbackService } from '../services/feedback.service';
import { FeedbackResponse, Theme } from '../models/message.models';
import { phoneValidator } from '../validators/phone.validator';

@Component({
  selector: 'app-feedback-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './feedback-form.component.html',
  styleUrl: './feedback-form.component.scss'
})
export class FeedbackFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly feedbackService = inject(FeedbackService);

  form!: FormGroup;
  themes: Theme[] = [];

  submittedMessage = signal<FeedbackResponse | null>(null);
  isLoading = signal(false);
  serverError = signal<string | null>(null);

  ngOnInit(): void {
    this.buildForm();
    this.loadThemes();
  }

  private buildForm(): void {
    this.form = this.fb.group({
      name:    ['', [Validators.required, Validators.maxLength(100)]],
      email:   ['', [Validators.required, Validators.email, Validators.maxLength(200)]],
      phone:   ['', [Validators.required, phoneValidator]],
      themeId: [null, [Validators.required, Validators.min(1)]],
      content: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(2000)]]
    });
  }

  private loadThemes(): void {
    this.feedbackService.getThemes().subscribe({
      next: (themes) => (this.themes = themes),
      error: () => this.serverError.set('Не удалось загрузить темы. Проверьте подключение.')
    });
  }

  get f() {
    return this.form.controls;
  }

  showError(field: string): boolean {
    const control = this.form.get(field);
    return !!control && control.invalid && control.touched;
  }

  onPhoneInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/[^\d+]/g, '');
    if (value && !value.startsWith('+')) {
      value = '+' + value;
    }
    this.form.get('phone')!.setValue(value, { emitEvent: false });
    input.value = value;
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.serverError.set(null);

    const payload = this.form.getRawValue();

    this.feedbackService.submit(payload).subscribe({
      next: (response) => {
        setTimeout(() => {
          this.submittedMessage.set(response);
          this.isLoading.set(false);
        }, 700);
      },
      error: (err) => {
        this.serverError.set(err.error?.error ?? 'Произошла ошибка. Попробуйте позже.');
        this.isLoading.set(false);
      }
    });
  }

  // Вернуться к форме и сбросить всё
  resetForm(): void {
    this.form.reset();
    this.submittedMessage.set(null);
    this.serverError.set(null);
  }
}
