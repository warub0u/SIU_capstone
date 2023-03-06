import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { GuardGuard } from './guard.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';
import { SchedulesComponent } from './schedules/schedules.component';
import { ForgetComponent } from './forget/forget.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { UpdatePasswordComponent } from './update-password/update-password.component';
import { AdminGuardGuard } from './admin-guard.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate:[GuardGuard]  },
  { path: '', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'admin', component: AdminComponent, canActivate:[AdminGuardGuard]},
  { path: 'profile/:username', component: ProfileComponent, canActivate:[GuardGuard]},
  { path: 'schedules', component: SchedulesComponent, canActivate:[GuardGuard]},
  { path: 'favorites', component: FavoritesComponent, canActivate:[GuardGuard]},
  { path: 'forget', component: ForgetComponent},
  { path: 'UpdatePassword', component: UpdatePasswordComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
