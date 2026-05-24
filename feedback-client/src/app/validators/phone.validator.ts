import { AbstractControl, ValidationErrors } from '@angular/forms';

// Маска в формате +7XXXXXXXXXX
export function phoneValidator(control: AbstractControl): ValidationErrors | null {
  const value: string = control.value ?? '';
  const clean = value.replace(/\s/g, '');
  const valid = /^\+7\d{10}$/.test(clean);
  return valid ? null : { invalidPhone: true };
}
