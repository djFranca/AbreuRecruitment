import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-gallery-modal',
  templateUrl: './create-gallery-modal.component.html',
  styleUrl: './create-gallery-modal.component.css'
})
export class CreateGalleryModalComponent implements OnInit {
  title: any;
  form: any;
  model: any;
  nameInvalid: boolean = true;
  cityInvalid: boolean = true;
  managerInvalid: boolean = true;

  constructor(private fb: FormBuilder, public dialogRef: MatDialogRef<CreateGalleryModalComponent>){
  }

  ngOnInit(): void {
    this.title = "Criar Galeria"
    this.form = this.fb.group({
      'id': '',
      'name': new FormControl('', [Validators.required, Validators.maxLength(100)]),
      'city': new FormControl('', [Validators.required, Validators.maxLength(100)]),
      'manager': new FormControl('', [Validators.required, Validators.maxLength(100)]),
      'nbrOfArtWorksOnDisplay': 0
    });
    this.model = {id: '', name: '', city: '', manager: '', nbrOfArtWorksOnDisplay: 0};
  }

  createGallery(): void{
    this.model = Object.assign(this.model, this.form.value);

    let invalidFields = this.CheckForInvalidFields();

    this.nameInvalid = invalidFields[0];
    this.cityInvalid = invalidFields[1];
    this.managerInvalid = invalidFields[2];

    if(invalidFields[0] === false && invalidFields[1] === false && invalidFields[2] === false){
      this.dialogRef.close(this.model);
    }
  }

  private CheckForInvalidFields() : boolean[] {
    let validationResults: boolean[] = [];

    validationResults[0] = this.model.name === null || this.model.name === '' || this.model.name === undefined;
    validationResults[1] = this.model.city === null || this.model.city === '' || this.model.city === undefined;
    validationResults[2] = this.model.manager === null || this.model.manager === '' || this.model.manager === undefined;

    return validationResults;
  }
}
