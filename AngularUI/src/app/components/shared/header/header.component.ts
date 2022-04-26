import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TokenModel } from 'src/app/models/TokenModel';
import { JwtManagerService } from 'src/app/services/JwtManagerService/jwtManager.service';
import { LoginComponent } from '../../login/login.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnChanges {
  isLoggedIn: boolean = false;
  decodedToken: TokenModel;
  constructor(
    private dialog: MatDialog,
    private jwtManager: JwtManagerService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.getTokenDetails();
  }

  getTokenDetails() {
    if (this.jwtManager.IsAuthenticated()) {
      this.isLoggedIn = true;
      this.decodedToken = this.jwtManager.decodedToken;
      console.log(this.decodedToken);
    } else this.isLoggedIn = false;
  }

  openLogin() {
    this.dialog
      .open(LoginComponent)
      .afterClosed()
      .subscribe(() => this.getTokenDetails());
  }

  logout() {
    this.isLoggedIn = false;
    this.jwtManager.Logout();
  }
}
