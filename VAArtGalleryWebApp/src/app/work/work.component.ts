import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Work } from './models';
import { WorkService } from './work.service';

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrl: './work.component.css'
})

export class WorkComponent implements OnInit {
  artGalleryId: string = '';
  works: Work[] = [];
  displayedColumns: string[] = ['name', 'author', 'creationYear', 'askPrice', 'actions'];
  tableHeaderColor: string = "";

  constructor(private workService: WorkService, private route: ActivatedRoute, private router: Router)
  {
    this.route.queryParams.subscribe(params => {
      this.artGalleryId = params['id']
    });
  }

  ngOnInit(): void {
    console.log("Art gallery id: " + this.artGalleryId);
    this.workService.getWorks(this.artGalleryId).subscribe(works => {this.works = works; console.log(this.works);});
    this.tableHeaderColor = "blue";
  }

  editWorkClick(workId: string){
    console.log(workId);
  }

  goToGalleries(){
    this.router.navigate(['art-galleries']);
  }
}
