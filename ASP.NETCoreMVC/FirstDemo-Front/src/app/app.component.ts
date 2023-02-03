import { Component } from '@angular/core';
import { CourseService } from './services/course.service';
import { ICourse } from './data/ICourse';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FirstDemo-Front';
  courses: ICourse[] = [];

  constructor(private courseService:CourseService){ 
  }

  update(){
    this.courses = this.courseService.getCourses();
  }

  printMessage1(){
    console.log("Button 1 clicked");
  }


  printMessage2(){
    console.log("Button 2 clicked");
  }
  
}
