import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface TrainingDTO {
  userId: string;
  trainingType: number;
  durationInSeconds: number;
  caloriesBurned: number;
  intensity: number;
  fatigue: number;
  notes: string;
  trainingDateTime: string;
}

@Injectable({
  providedIn: 'root'
})
export class TrainingService {
  private apiUrl = `${environment.apiUrl}/Training`;

  constructor(private http: HttpClient) {}

  addTraining(training: TrainingDTO): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/add`, training);
  }
}
