import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Gallery } from './models';
import { GalleryService } from './gallery.service';



@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css'
})

export class GalleryComponent implements OnInit {
  galleries: Gallery[] = [];
  displayedColumns: string[] = ['name', 'city', 'manager', 'nbrWorks', 'actions'];
  tableHeaderColor: string = "";

  constructor(private galleryService: GalleryService) { }

  ngOnInit(): void {
    console.log('cenas');
    this.galleryService.getGalleries().subscribe(galleries => {this.galleries = galleries; console.log(this.galleries);});
    this.tableHeaderColor = "blue";
  }

  editGalleryClick(galleryId: string) {
    console.log(galleryId);
  }

  openArtWorksList(galleryId: string) {
    console.log(galleryId);
  }
}
