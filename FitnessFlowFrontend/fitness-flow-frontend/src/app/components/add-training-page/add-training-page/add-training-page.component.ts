import { Component } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar/navbar.component";
import { MatSliderModule } from '@angular/material/slider';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog'
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule} from '@angular/common';
import { TrainingService } from '../../../services/training/training.service';
import { AuthService } from '../../../services/auth/auth.service';
import { DialogComponent } from '../../dialog/dialog/dialog.component';

@Component({
  selector: 'app-add-training-page',
  standalone: true,
  imports: [ReactiveFormsModule, NavbarComponent,  MatSliderModule, MatDatepickerModule, MatInputModule,
    MatSelectModule, MatNativeDateModule, MatFormFieldModule, MatButtonModule, CommonModule, MatDialogModule
   ],
  templateUrl: './add-training-page.component.html',
  styles: ``
})

export class AddTrainingPageComponent {
  trainingForm: FormGroup;
  isSubmitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private trainingService: TrainingService,
    private authService: AuthService,
    private dialog: MatDialog
  ) {
    this.trainingForm = this.formBuilder.group({
      trainingType: ['', [Validators.required]],
      caloriesBurned: ['', [Validators.required, Validators.min(0)]],
      durationHours: ['', [Validators.min(0)]],
      durationMinutes: ['', [Validators.min(0), Validators.max(59)]],
      durationSeconds: ['', [Validators.min(0), Validators.max(59)]],
      intensity: [1, [Validators.required, Validators.min(1), Validators.max(10)]],
      fatigue: [1, [Validators.required, Validators.min(1), Validators.max(10)]],
      notes: [''],
      date: ['', [Validators.required]],
      hours: ['', [Validators.required, Validators.min(0), Validators.max(23)]],
      minutes: ['', [Validators.min(0), Validators.max(59)]]
    });
  }

  get formControls() { return this.trainingForm.controls; }

  openDialog(title: string, message: string): void {
    this.dialog.open(DialogComponent, {
      data: {
        title: title,
        message: message
      }
    });
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.trainingForm.valid) {
      const formValue = this.trainingForm.value;
      const durationInSeconds = (formValue.durationHours * 3600) + (formValue.durationMinutes * 60) + formValue.durationSeconds;

      if (durationInSeconds <= 0) {
        this.openDialog('Invalid Duration', 'The training duration must be greater than 0 seconds.');
        return;
      }

      const date = formValue.date;
      const hours = formValue.hours.toString().padStart(2, '0');
      const minutes = formValue.minutes.toString().padStart(2, '0');
      const trainingDateTime = new Date(date.getFullYear(), date.getMonth(), date.getDate(), parseInt(hours), parseInt(minutes), 0);
      const trainingDateTimeUTC = trainingDateTime.toISOString();

      const training = {
        userId: this.authService.getUserIdFromToken(),
        trainingType: parseInt(formValue.trainingType, 10),
        durationInSeconds: parseInt(durationInSeconds.toString(), 10),
        caloriesBurned: parseInt(formValue.caloriesBurned, 10),
        intensity: parseInt(formValue.intensity, 10),
        fatigue: parseInt(formValue.fatigue, 10),
        notes: formValue.notes,
        trainingDateTime: trainingDateTimeUTC
      };

      console.log(training);
      this.trainingService.addTraining(training).subscribe(
        response => {
          console.log('Training added successfully', response);
          this.openDialog('Success', 'Training added successfully.');
          this.isSubmitted = false;
          this.trainingForm.reset();
        },
        error => {
          console.error('Error adding training', error.error);
          this.openDialog('Error', 'Failed to add training. Please try again.');
        }
      );
    
    }
  }


}
