import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, FormsModule,ReactiveFormsModule } from "@angular/forms";
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/Services/authorize.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'register-modal',
    templateUrl: 'registerModal.component.html',
    styleUrls:['registerModal.component.scss'],
    standalone:true,
    imports:[
        MatCheckboxModule,
        ReactiveFormsModule,
        CommonModule,
        HttpClientModule,
        MatButtonModule,
        
    ]
})
export class registerModal implements OnInit, OnDestroy {
    constructor(private authService:AuthService, private formBuilder:FormBuilder ) { }
    ngOnDestroy(): void {
        this.subs.unsubscribe()
    }
    private subs = new Subscription();

    public registerForm = this.formBuilder.group({
        username: ["",[
            Validators.minLength(3),
            Validators.required
        ]],
        password: ["", [
            Validators.minLength(8),
            Validators.required
        ]],
        passwordConfirm: ["", [
            Validators.minLength(8),
            Validators.required
        ]]
    }, this.passwordMatchValidator);

    public passwordMatchValidator(g: FormGroup) {
        return g.get('password')?.value === g.get('passwordConfirm')?.value
           ? null : {'mismatch': true};
    }

    public usernameValue = this.authService.getUsername();
    public passValue = this.authService.getPassword();

    get loginOnRegister(){ return this.authService.getLoginOnRegister().value }
    set loginOnRegister(value:boolean){ this.authService.setLoginOnRegister(value) }

    get username(){ return this.registerForm.get("username") }
    get password(){ return this.registerForm.get("password") }

    ngOnInit():void { 
    }

    public register(){
        let userna = this.usernameValue.getValue()
        let pass = this.passValue
        if (userna && pass){
            this.subs.add(this.authService.register(userna,pass).subscribe({
                next:res=>{
                    console.log("Registration Complete!");
                    console.log(res);         
                },
                complete: ()=>{
                    if(this.loginOnRegister == true) {
                        this.subs.add(this.authService.login(userna, pass).subscribe((val)=>{
                            if(val){
                                console.log("Successful Login!");
                                console.log(val);
                            }
                        }))
                    }
                }
            }))
        }
    }
}