import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from "../../navbar/navbar/navbar.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TrainingDTO, TrainingService, TrainingStatsDTO } from '../../../services/training/training.service';
import { AuthService } from '../../../services/auth/auth.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../../dialog/dialog/dialog.component';

@Component({
  selector: 'my-trainings',
  standalone: true,
  imports: [NavbarComponent, ReactiveFormsModule, MatFormFieldModule,
            CommonModule, MatInputModule, MatSelectModule, MatDialogModule
  ],
  templateUrl: './my-trainings-page.component.html',
  styles: ``
})
export class MyTrainingsPageComponent {
  trainingStatsForm: FormGroup;
  trainingStats: TrainingStatsDTO[] = [];
  dataFetched = false;
  

  constructor(
    private formBuilder: FormBuilder,
    private trainingService: TrainingService,
    private authService: AuthService,
    private dialog: MatDialog

  ) {
    this.trainingStatsForm = this.formBuilder.group({
      year: [null, Validators.required],
      month: [null,  Validators.required],
    });
  }

  openDialog(title: string, message: string): void {
    this.dialog.open(DialogComponent, {
      data: { title, message }
    });
  }

  fetchTrainingStats() {
    const userId = this.authService.getUserIdFromToken();
    const { year, month} = this.trainingStatsForm.value;

    if (!year || !month) {
      this.openDialog('Missing Information', 'Please select both the year and the month to proceed.');
      return;
    }

    this.trainingService.getTrainingStats(userId, year, month).subscribe(
      (stats) => {
        this.trainingStats = stats;
        this.dataFetched = true;
      },
      (error) => {
        console.error('Error fetching training stats', error);
      }
    );
  }

  onFiltersChange() {
    this.fetchTrainingStats();
  }

  getWeekStats(weekNumber: number): TrainingStatsDTO | undefined {
    return this.trainingStats.find(stat => stat.weekNumber === weekNumber);
  }

  formatDuration(durationInSeconds: number): string {
    if (durationInSeconds == null) return '0 hours 0 minutes 0 seconds';
  
    const hours = Math.floor(durationInSeconds / 3600);
    const minutes = Math.floor((durationInSeconds % 3600) / 60);
    const seconds = durationInSeconds % 60;
  
    let result = '';
    if (hours > 0) result += `${hours} hours `;
    if (minutes > 0 || hours > 0) result += `${minutes} minutes `;
    if (seconds > 0 || minutes > 0 || hours > 0) result += `${seconds} seconds`;
  
    return result.trim();
  }
  
}
