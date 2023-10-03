import { Component } from '@angular/core';
import {FormControl, FormGroup , FormBuilder, Validators} from '@angular/forms';

@Component({
  selector: 'news-letter-container',
  templateUrl: './news-letter-container.component.html',
  styleUrls: ['./news-letter-container.component.css']
})
export class NewsLetterContainerComponent {
  newsLetterForm = new FormGroup({
    fullName : new FormControl(''),
    email : new FormControl('')
  });
  submitted = false;
   constructor(private fromBuilder: FormBuilder){ }
   
   ngOnInit(){
    //validations
    this.newsLetterForm = this.fromBuilder.group({
      fullName:['',Validators.required],
      email :['',Validators.required]
    })
   }
   Onsubmit(){
    this.submitted = true;
    if(this.newsLetterForm.invalid){
      return
    }
    alert("Success")
   }
}
