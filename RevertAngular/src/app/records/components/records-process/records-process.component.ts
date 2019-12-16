import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RecordsService } from '../../services/records.service';
import { Router } from '@angular/router';
import { Record } from '../../models/Record';
import { ProcessingService } from '../../services/processing.service';

@Component({
  selector: 'app-records-process',
  templateUrl: './records-process.component.html',
  styleUrls: ['./records-process.component.css']
})
export class RecordsProcessComponent implements OnInit {
  form: FormGroup;

  constructor(private api: ProcessingService, private router: Router) {
    this.form = new FormGroup({
      stareaGenerala: new FormControl(''),
      talie: new FormControl('', Validators.required),
      greutate: new FormControl('', Validators.required),
      inaltime: new FormControl('', Validators.required),
      sex: new FormControl('', Validators.required),
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
      istoriculBolii: new FormControl('')
    });
  }

  onSubmit() {
    this.api.process(this.form.value as Record).subscribe(response => {
      localStorage.setItem('diagnosis', JSON.stringify(response.diagnosis));
      localStorage.setItem('lastRecord', JSON.stringify(this.form.value as Record));

      this.router.navigate(['/records', 'process', 'result']);
    });
  }

  onCancel() {
    this.router.navigate(['records']);
  }

  ngOnInit() { }
}
