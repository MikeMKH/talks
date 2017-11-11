import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CoolService } from '../cool.service';

@Component({
  selector: 'mke-cool-list',
  templateUrl: './cool-list.component.html',
  styleUrls: ['./cool-list.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class CoolListComponent implements OnInit {
  public items: string[];
  constructor(private service: CoolService) { }

  ngOnInit() {
    this.items = this.service.getLanguages();
  }

}
