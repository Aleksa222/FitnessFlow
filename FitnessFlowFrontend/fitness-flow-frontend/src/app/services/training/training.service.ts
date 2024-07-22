import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environments';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TrainingDTO {
  userId: string;
  trainingType: number;
  durationInSeconds: number;
  caloriesBurned: number;
  intensity: number;
  fatigue: number;
  notes: string;
  trainingDateTime: string;
}

export interface TrainingStatsDTO {
  weekNumber: number;
  totalDurationInSeconds: number;
  numberOfTrainings: number;
  averageIntensity: number;
  averageFatigue: number;
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

  getTrainings(userId: string): Observable<TrainingDTO[]> {
    return this.http.get<TrainingDTO[]>(`${this.apiUrl}/get/${userId}`);
  }

  getTrainingStats(userId: string, year?: number, month?: number, week?: number): Observable<TrainingStatsDTO[]> {
    let params = new HttpParams();
    if (year !== undefined && year !== null) params = params.set('year', year.toString());
    if (month !== undefined && month !== null) params = params.set('month', month.toString());
    if (week !== undefined && week !== null) params = params.set('week', week.toString());
    return this.http.get<TrainingStatsDTO[]>(`${this.apiUrl}/get/${userId}/stats`, { params });
  }
  
}
