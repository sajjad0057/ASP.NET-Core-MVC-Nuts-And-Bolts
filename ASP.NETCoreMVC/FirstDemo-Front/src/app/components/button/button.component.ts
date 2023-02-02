import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent implements OnInit {
  @Input() text:string;
  @Input() redBg:string;

  constructor(){
    this.text = "";
    this.redBg = ""
  }

  ngOnInit(): void {
    
  }

  onClick(){
    console.log("button clicked !")
  }
}
