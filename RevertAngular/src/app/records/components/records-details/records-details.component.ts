import { Component, OnInit } from '@angular/core';
import { RecordsService } from '../../services/records.service';
import { Router, ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Record } from '../../models/Record';
import { Keyword } from '../../models/Keyword';

@Component({
  selector: 'app-records-details',
  templateUrl: './records-details.component.html',
  styleUrls: ['./records-details.component.css']
})
export class RecordsDetailsComponent implements OnInit {
  model: Record;
  recordId: string;
  data = [];

  constructor(private api: RecordsService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.recordId = this.route.snapshot.params.id;

    this.api.getById(this.recordId).subscribe(response => {
      this.model = response;
      Object.keys(this.model).forEach(key => {
        if (!!this.model[key] && key !== 'id' && key !== 'creationDate' && key !== 'keywords') {
          this.data.push({name: key.split(/(?=[A-Z])/).join(' '), value: this.model[key]});
        }
      });
    });
  }

  getKeywordCategories(): string[] {
    return [...new Set(this.model.keywords.map(k => k.name))];
  }

  getKeywordByName(name): Keyword[] {
    return this.model.keywords.filter(k => k.name === name);
  }

  onDelete() {
    this.api.delete(this.recordId).subscribe(response => {
      this.router.navigate(['/records']);
    });
  }
}
