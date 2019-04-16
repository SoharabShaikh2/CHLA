import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validator, Validators } from '@angular/forms';
import { UserService } from '../../services/user-service/user.service';
import { Router } from "@angular/router";

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
    ])),
    resetcode: new FormControl('', Validators.compose([
      Validators.required,
      //Validators.email,
      //Validators.pattern('\w+?@\w+?\x2E.+'),
    ]))
  });

  constructor(private user: UserService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(f) {
    this.user.setPassword(f.resetcode, f.password).subscribe(data => {
      if (data.status == 1) {
        this.router.navigate(['/']);
      }
      else {
        alert(data.error);
      }
      console.log(JSON.stringify(data));
    });
  }

}
