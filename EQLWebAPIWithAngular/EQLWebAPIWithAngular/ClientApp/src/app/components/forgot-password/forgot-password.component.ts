import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validator, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponentReset implements OnInit {

  form = new FormGroup({
    password: new FormControl('', Validators.compose([
      Validators.required,
      //Validators.email,
      //Validators.pattern('\w+?@\w+?\x2E.+'),
    ]))
  });

  constructor() { }

  ngOnInit() {
  }

}
