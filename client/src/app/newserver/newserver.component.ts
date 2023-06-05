import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-newserver',
  templateUrl: './newserver.component.html',
  styleUrls: ['./newserver.component.scss']
})
export class NewserverComponent implements OnInit {
  public createForm!: FormGroup;
  public serverJars!: string[];

  constructor(private formBuilder: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.getServerJars();

    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      serverJar: ['', Validators.required]
      // Add more form controls as needed
    });
  }

  create() {
    // Get the form values
    const formValues = this.createForm.value;
    const serverName = formValues.name;
    const serverEmail = formValues.email;
    const selectedJar = formValues.serverJar;

    // TODO: Logic to create the server using the form values
  }

  cancel() {
    // TODO: Logic for cancel button
  }

  getServerJars() {
    this.http.get<string[]>('/serverjars').subscribe(
      (response) => {
        this.serverJars = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
