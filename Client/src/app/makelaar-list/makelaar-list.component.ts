import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { tap } from 'rxjs/operators';
import { Makelaar } from '../models/makelaar.model';
import { MakelaarService } from '../services/makelaar.service';

@Component({
  selector: 'app-makelaar-list',
  templateUrl: './makelaar-list.component.html',
  styleUrls: ['./makelaar-list.component.scss']
})
export class MakelaarListComponent implements OnInit {

  @Input() tuin!: boolean;
  displayedColumns: string[] = ['Place', 'Name', 'AanbodCount'];
  makelaars: Makelaar[] = [];
  
  constructor(private service: MakelaarService) { }

  ngOnInit(): void {
    this.service.getMakelaars(this.tuin).subscribe((makelaars) => {
      this.makelaars = makelaars;
    });
  }

}
