import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GalleryComponent } from './gallery/gallery.component';
import { WorkComponent } from './work/work.component';

const routes: Routes = [
  { path: '', component: GalleryComponent },
  { path: 'art-galleries', component: GalleryComponent },
  { path: 'art-works', component: WorkComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
