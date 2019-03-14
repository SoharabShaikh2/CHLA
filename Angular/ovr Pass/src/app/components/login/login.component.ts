import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormControl, Validator, Validators } from '@angular/forms';
import {UserService} from '../../services/user-service/user.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  hide = true;
  users: Object;
  //matcher = new MyErrorStateMatcher();
  form = new FormGroup({
    email: new FormControl('', Validators.compose([
      Validators.required,
      Validators.email,
      //Validators.pattern('\w+?@\w+?\x2E.+'),
    ])),
    password: new FormControl('', Validators.compose([
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(24),
    ])),
  });
  constructor(private user :UserService) { }

  ngOnInit() {
  }
  userLogin(){
    
  }
  onSubmit(f) {
    this.user.checkUser(f.email,f.password).subscribe(data => {
      console.log(data);
    });
  }
}
