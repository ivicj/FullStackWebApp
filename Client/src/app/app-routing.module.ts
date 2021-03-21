import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MakelaarListComponent } from './makelaar-list/makelaar-list.component';

const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  { path: 'makelaars', component: MakelaarListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
