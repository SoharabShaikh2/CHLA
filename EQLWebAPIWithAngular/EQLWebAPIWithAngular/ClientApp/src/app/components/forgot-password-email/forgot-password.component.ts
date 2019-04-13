import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validator, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  form = new FormGroup({
    email: new FormControl('', Validators.compose([
      Validators.required,
      //Validators.email,
      //Validators.pattern('\w+?@\w+?\x2E.+'),
    ]))
  });

  constructor() { }

  ngOnInit() {
  }

}
