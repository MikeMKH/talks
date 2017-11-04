import { Component, OnInit } from '@angular/core';
import { CoolService } from '../services/cool.service';

@Component({
  selector: 'mke-cool-list',
  templateUrl: './cool-list.component.html',
  styleUrls: ['./cool-list.component.css']
})
export class CoolListComponent implements OnInit {
  languages: string[];

  constructor(private serivce: CoolService) { }

  ngOnInit() {
    this.serivce.getLanguages()
      .subscribe(next => this.languages = next);
  }

}
