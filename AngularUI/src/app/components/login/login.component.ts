import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { JwtManagerService } from 'src/app/services/JwtManagerService/jwtManager.service';
import { LoginService } from 'src/app/services/loginService/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private loginService: LoginService,
    private jwtManager: JwtManagerService,
    private dialogRef: MatDialogRef<LoginComponent>,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadForm();
  }

  login() {
    console.log(this.loginForm.value);
    this.loginService.verifyUser(this.loginForm.value).subscribe({
      next: (result) => {
        this.jwtManager.SetToken(result.data);
      },
      error: (err) => {
        console.error(err);
        this.toastr.error('Please Try Again', 'Login Failed');
      },
      complete: () => {
        this.dialogRef.close();
      },
    });
  }

  loadForm() {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  get f() {
    return this.loginForm.controls;
  }
}
