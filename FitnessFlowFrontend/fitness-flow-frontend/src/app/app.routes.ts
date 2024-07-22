import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { HomePageComponent } from './components/home-page/home-page/home-page.component';
import { UserAuthGuard } from './guards/user-auth-guard';
import { LoginComponent } from './components/login/login.component';
import { AddTrainingPageComponent } from './components/add-training-page/add-training-page/add-training-page.component';
import { MyTrainingsPageComponent } from './components/my-trainings-page/my-trainings-page/my-trainings-page.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent},
    { path: 'register', component: RegisterComponent },
    { path: 'home', component: HomePageComponent, canActivate: [UserAuthGuard]},
    { path: 'add-training', component: AddTrainingPageComponent, canActivate: [UserAuthGuard]},
    { path: 'my-trainings', component: MyTrainingsPageComponent, canActivate: [UserAuthGuard]}
    

];
