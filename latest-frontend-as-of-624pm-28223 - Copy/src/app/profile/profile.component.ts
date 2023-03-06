import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { UpdatePasswordComponent } from '../update-password/update-password.component';
import { Data, Fav2, password } from '../Model/model';
import { DataService } from '../service/data.service';
import { environment } from '../environment/Environment';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
  constructor(
    private service: DataService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    @Inject(DOCUMENT) private document: Document,
    private fb: FormBuilder,
    private route2: Router,
    private snackBar: MatSnackBar
  ) {}
  username: any = '';
  user: Array<Data> = [];
  favList: Array<Fav2> = [];
  userObj: any = {};

  ngOnInit() {
    //to intialise the form with all the fields but they will be empty
    this.createForm();
    this.username = this.route.snapshot.paramMap.get('username');
    this.service.specificUser(this.username).subscribe((data: any) => {
      this.user.push(data);
      this.userObj = data;
      this.createForm();
      //at this stage, we have the userObj, so we can now populate the fields
    });
    this.getProfilePic();
  }

  isLoading: boolean = false;

  passwordForm = this.fb.group({
    new: new FormControl('', [Validators.required, Validators.minLength(6)]),
    current: new FormControl('', [Validators.required]),
    confirm: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });

  get new() {
    return this.passwordForm.get('new');
  }

  get current() {
    return this.passwordForm.get('current');
  }

  get confirm() {
    return this.passwordForm.get('confirm');
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {});
  }

  pass: password = {};
  updatePass() {
    if (this.passwordForm.valid) {
      this.pass.userName = localStorage.getItem('username');
      this.pass.old_pw = this.passwordForm.controls['current'].value;
      this.pass.new_pw = this.passwordForm.controls['new'].value;
      this.service.updatePassword(this.pass).subscribe((data) => {
        this.snackBar.open('Password Updated! Please relog in.', 'close', {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
        localStorage.clear();
        this.route2.navigateByUrl('/login');
        this.service.isloggedIn.next(false);
      }),
        (err: HttpErrorResponse) => {
          this.snackBar.open(err.error, 'close', {
            horizontalPosition: 'center',
            verticalPosition: 'top',
            duration: 3000,
          });
        };
    } else {
      this.snackBar.open(
        'Update unsuccessful, please check form fields.',
        'close',
        {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        }
      );
    }
  }
  //response will = event , event from upload component will contain dbpath. dbpath is the path of where the image is stored
  response: any = {};

  uploadFinished(event: any) {
    this.response = event;
    var re = /\\\\/gi;
    const newPath = this.response.dbPath.replace(re, '/');
    this.response.dbPath = environment.authUrl + `/${newPath}`;
    this.getProfilePic();
  }

  getProfilePic() {
    this.service.getPicture(localStorage.getItem('username') || '').subscribe(
      (data) => {
        this.createImageFromBlob(data);
        this.service.passPicture.next(this.response);
      },
      (error) => {
        this.response = {};
        //if we get an error, this.response = null. Will display default picture provided
      }
    );
  }
  //converting blob from getProfilePic into the base64-encoded source
  createImageFromBlob(data: Blob) {
    //FileReader reads blob data and
    let reader = new FileReader();
    //this method of FileReader set where image is saved ->  at response.dbPath
    reader.addEventListener('load', () => {
      this.response.dbPath = reader.result;
    });
    //if able to get data from the get request, this method will convert the blob into encoded source
    if (data) {
      reader.readAsDataURL(data);
    }
  }

  openDialog(): void {
    let dialogRef = this.dialog.open(UpdatePasswordComponent, {
      width: '400px',
      data: {},
    });
  }

  //only create form
  createForm() {
    this.updateForm = this.fb.group({
      firstName: new FormControl(this.userObj.firstName, Validators.required),
      lastName: new FormControl(this.userObj.lastName, Validators.required),
      mobileNo: new FormControl(this.userObj.mobileNo, [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(8),
      ]),
      postalCode: new FormControl(this.userObj.postalCode, [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(6),
      ]),
      email: new FormControl(this.userObj.email),
      isBlocked: new FormControl(this.userObj.isBlocked),
      dob: new FormControl(this.userObj.dob),
      role: new FormControl(this.userObj.role),
      userName: new FormControl(this.userObj.userName),
      gender: new FormControl(this.userObj.gender),
      isPasswordReset: new FormControl(this.userObj.isPasswordReset),
      password: new FormControl(this.userObj.password),
    });
  }
  //updateForm of type FormGroup will be initialised else where aka at ngOnInit
  updateForm!: FormGroup;
  get postalCode() {
    return this.updateForm.get('postalCode');
  }
  get mobileNo() {
    return this.updateForm.get('mobileNo');
  }

  get firstName() {
    return this.updateForm.get('firstName');
  }
  get lastName() {
    return this.updateForm.get('lastName');
  }
  update() {
    //forcing update.form.value into interface data
    const data = this.updateForm.value as Data;
    this.service.updateInfo(data).subscribe((data: any) => {
      this.user.push(data);
      this.userObj = data;
    });
  }
}
