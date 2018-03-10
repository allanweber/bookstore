import { Component, OnInit } from '@angular/core';
import { Livro } from './shared/models/livro.model';

@Component({
  selector: 'app-livros',
  templateUrl: './livros.component.html',
  styleUrls: ['./livros.component.css']
})
export class LivrosComponent implements OnInit {

  public livros: Livro[];

  constructor() { }

  ngOnInit() {
  }

}
