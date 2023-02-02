import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FirstDemo-Front';


  printMessage1(){
    console.log("Button 1 clicked");
  }


  printMessage2(){
    console.log("Button 2 clicked");
  }
  
}
