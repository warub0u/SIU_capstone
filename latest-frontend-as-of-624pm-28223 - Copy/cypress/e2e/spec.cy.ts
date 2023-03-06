import { MatSnackBar } from "@angular/material/snack-bar";
import * as cypress from "cypress"

describe('Testing registration', () => {
  it('Register user should succeed', () => {
    cy.visit('http://localhost:4200/register');
    cy.get('.fname').type('Lionel');
    cy.get('.lname').type('Messi');
    cy.get('.uname').type('messi10');
    cy.get('.birthdate').type('1987-06-01', {force:true});
    cy.get('.mobileNo').type('98888888', {force:true});
    cy.get('.postal').type('530000');
    cy.get('.definegender').contains('Male').click();
    cy.get('.email').type('lionel10@gmail.com');
    cy.get('.pw').type('messii');
    cy.get('.my-button').click();
    cy.wait(1000);

    cy.url().should('eq', 'http://localhost:4200/')
  })
})

describe('Testing registration', () => {
  it('Register user should fail', () => {
    cy.visit('http://localhost:4200/register');
    cy.get('.fname').type('Lionel');
    cy.get('.lname').type('Messi');
    cy.get('.uname').type('messi10');
    cy.get('.birthdate').type('1987-06-01', {force:true});
    cy.get('.mobileNo').type('999', {force:true});
    cy.get('.postal').type('530000');
    cy.get('.definegender').contains('Male').click();
    cy.get('.email').type('lionel10@gmail.com');
    cy.get('.pw').type('mess');
    cy.get('.my-button').click();
    cy.wait(1000);
    cy.contains('Registeration unsuccessful. Please check form fields.').should('be.visible');
    //cy.get('.mat-simple-snack-bar-content').should('have.text', 'Registeration unsuccessful. Please check form fields.');

  })
})


describe('Testing login', () => {
  it('Login user should fail', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('MESSI');
    cy.get('.btn').click();
    cy.wait(1000);
    cy.contains('Your username/password is wrong!').should('be.visible');

    //cy.get('.mat-simple-snack-bar-content').should('have.text', 'Your username/password is wrong!');
  })
})


describe('Testing login', () => {
  it('Login user should succeed', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('messii');
    cy.get('.btn').click();
    cy.wait(1000);

    cy.url().should('eq', 'http://localhost:4200/profile/messi10')
  })
})


// describe('Testing update user details', () => {
//   it('Update user should succeed', () => {
//     cy.visit('http://localhost:4200/profile/messi10');
//     cy.get('.fname').type('Leo');
//     cy.get('.lname').type('Messi');
//     cy.get('.mobile').type('98000018');
//     cy.get('.postal').type('570178');  
//     cy.wait(3000);

//     cy.get('.mat-simple-snack-bar-content').should('have.text', 'user registered successfully');
//   })
// })


describe('Testing update user details', () => {
  it('Update user password should succeed', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('messii');
    cy.get('.btn').click();
    cy.wait(1000);
    
    cy.get('.pw-button').click();

    cy.get('.oldpw').type('messii');
    cy.get('.newpw1').type('123456', {force:true});
    cy.get('.newpw2').type('123456', {force:true});
    cy.get('.pwsubmitbtn').click();
    cy.wait(1000);

    cy.contains('Password updated!').should('be.visible');

    //cy.get('.mat-simple-snack-bar-content').should('have.text', 'Password updated!');
  })
})


describe('Testing sidenav header', () => {
  it('clicking header "Directions" should succeed', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('123456');
    cy.get('.btn').click();
    cy.wait(1000);

    cy.get('.sidenav').click();
    cy.get('.directions').click();     
    cy.wait(1000);
    cy.url().should('eq', 'http://localhost:4200/home')
    
  })
})


describe('Testing sidenav header', () => {
  it('clicking header "Schedules" should succeed', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('123456');
    cy.get('.btn').click();
    cy.wait(1000);

    cy.get('.sidenav').click();
    cy.get('.schedule').click();     
    cy.wait(1000);
    cy.url().should('eq', 'http://localhost:4200/schedules')
    
  })
})


describe('Testing sidenav header', () => {
  it('clicking header "Admin" should fail', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('123456');
    cy.get('.btn').click();
    cy.wait(1000);

    cy.get('.sidenav').click();
    cy.get('.adminr').click();     
    cy.wait(500);
    cy.contains('You do not have access to this page').should('be.visible');   
    
  })
})


describe('Testing schedules', () => {
  it('getting bus schedule for bus stop 53009 should retrieve bus 410W', () => {
    cy.visit('http://localhost:4200');
    cy.get('.usernamebox').type('messi10');
    cy.get('.pwbox').type('123456');
    cy.get('.btn').click();
    cy.wait(1000);

    cy.get('.sidenav').click();
    cy.get('.schedule').click();     
    cy.wait(1000);

    cy.get('.busstopinput').type('53009', {force:true});
    cy.get('.busbtn').click({force: true});
   
    cy.contains('410W').should('be.visible');       
  })
})