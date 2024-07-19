import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Gallery } from './models';
import { GalleryService } from './gallery.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CreateGalleryModalComponent } from '../create-gallery-modal/create-gallery-modal.component';


@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css'
})

export class GalleryComponent implements OnInit {
  galleries: Gallery[] = [];
  displayedColumns: string[] = ['name', 'city', 'manager', 'nbrWorks', 'actions'];
  tableHeaderColor: string = "";
  gallery: Gallery = {id: '', name: '', city: '', manager: '', nbrOfArtWorksOnDisplay: 0 };

  constructor(private galleryService: GalleryService, private router: Router, private cd: ChangeDetectorRef, private modal: MatDialog) { }

  ngOnInit(): void {
    console.log('cenas');
    this.getGalleries();
    this.tableHeaderColor = "blue";
  }

  getGalleries(){
    this.galleryService.getGalleries().subscribe(galleries => {this.galleries = galleries; console.log(this.galleries);});
  }

  editGalleryClick(galleryId: string) {
    console.log(galleryId);
  }

  openArtWorksList(galleryId: string) {

    console.log(galleryId);

    this.router.navigate(['/art-works'], {queryParams: {id: galleryId}});
  }

  openCreateGalleryModal() {
    console.log("Create gallery modal");

    const modal = this.modal.open(CreateGalleryModalComponent, {
      width: '600px',
      height: '600px'
    });

    modal.afterClosed().subscribe((result) => {
      this.gallery.name = result.name;
      this.gallery.city = result.city;
      this.gallery.manager = result.manager;

      this.addGallery();
    })
  }

  addGallery(){
    this.galleryService.addGallery(this.gallery).subscribe((result) => {
      if (result !== null){
        this.getGalleries();
      }
    });
  }
}
