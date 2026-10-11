import { Routes } from '@angular/router';

import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Dashboard } from './pages/dashboard/dashboard';
import { Pets } from './pages/pets/pets';
import { Policies } from './pages/policies/policies';
import { Claims } from './pages/claims/claims';
import { Profile } from './pages/profile/profile';
import { ImportPage } from './pages/import/import';
import { ExportPage } from './pages/export/export';
export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: Login
  },

  {
    path: 'register',
    component: Register
  },

  {
    path: 'dashboard',
    component: Dashboard
  },

  {
    path: 'pets',
    component: Pets
  },

  {
    path: 'policies',
    component: Policies
  },

  {
    path: 'claims',
    component: Claims
  },

  {
    path: 'profile',
    component: Profile
  },
  {
  path: 'import',
  component: ImportPage
},
{
  path: 'export',
  component: ExportPage
}
];
