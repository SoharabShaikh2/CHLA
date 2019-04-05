import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormControl, Validator, Validators } from '@angular/forms';
import {UserService} from '../../services/user-service/user.service';
import {Router} from "@angular/router";
import {Globals} from '../../global';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  hide = true;
  users: Object;
  private loginStatus: boolean;
  //matcher = new MyErrorStateMatcher();
  form = new FormGroup({
    email: new FormControl('', Validators.compose([
      Validators.required,
      //Validators.email,
      //Validators.pattern('\w+?@\w+?\x2E.+'),
    ])),
    password: new FormControl('', Validators.compose([
      Validators.required,
      //Validators.minLength(6),
      //Validators.maxLength(24),
    ])),
  });
  constructor(private user :UserService, private router: Router,private globals: Globals) {
    this.loginStatus = this.globals.loginStatus;
   }

  ngOnInit() {
    this.globals.loginStatus = false;
  }
  userLogin(){
    
  }
  onSubmit(f) {
    this.user.checkUser(f.email,f.password).subscribe(data => {

      console.log(data);
      if (data.status == true)
      {
        this.globals.loginStatus = true;
        if(data.data.usertypeid == 4)
        {
          this.router.navigate(['/organizationList']);
        }
        else if(data.data.usertypeid == 1)
        {
          this.router.navigate(['/admin']);
        }
        else if(data.data.usertypeid == 3)
        {
          this.router.navigate(['/drModule']);
        }

      }
      else
      {
        alert(data.error);
      }
      console.log(JSON.stringify(data));
    });
  }
}
