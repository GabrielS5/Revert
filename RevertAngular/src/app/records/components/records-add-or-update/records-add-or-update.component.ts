import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '../../../../../node_modules/@angular/forms';
import { RecordsService } from '../../services/records.service';
import { Record } from '../../models/Record';
import { Router } from '../../../../../node_modules/@angular/router';

@Component({
  selector: 'app-records-add-or-update',
  templateUrl: './records-add-or-update.component.html',
  styleUrls: ['./records-add-or-update.component.css']
})
export class RecordsAddOrUpdateComponent implements OnInit {
  form: FormGroup;

  constructor(private api: RecordsService, private router: Router) {
    this.form = new FormGroup({
      stareaGenerala: new FormControl(''),
      talie: new FormControl(''),
      greutate: new FormControl(''),
      nutritie: new FormControl(''),
      constienta: new FormControl(''),
      facies: new FormControl(''),
      tegumente: new FormControl(''),
      mucoase: new FormControl(''),
      fanere: new FormControl(''),
      tesutConjunctiv: new FormControl(''),
      sistemGanglionar: new FormControl(''),
      sistemMuscular: new FormControl(''),
      sistemOsteoArticular: new FormControl(''),
      aparatRespirator: new FormControl(''),
      aparatCardiovascular: new FormControl(''),
      aparatDigestiv: new FormControl(''),
      ficatSplina: new FormControl(''),
      aparatUroGeneral: new FormControl(''),
      sistemEndocrin: new FormControl(''),
      motiveleInternarii: new FormControl(''),
      anamneza: new FormControl(''),
      istoriculBolii: new FormControl(''),
      diagnosis: new FormControl('')
    });
  }

  onSubmit() {
    this.api.post(this.form.value as Record).subscribe(response => {
      this.router.navigate(['records']);
    });
  }

  onCancel() {
    this.router.navigate(['records']);
  }

  ngOnInit() { }
}
