import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'testApp';
  inputArray = [];

  addInput(value) {
  	if(value != "") {
	  	this.inputArray.push(value); 
  	}
  }

  inputSubmit(value) {
  	console.log(value);
  	if(value != "") {
	  	this.inputArray.push(value.inputTest);
  	}
  }

  deleteInput(value) {
  	// manual search + remove
  	for(let i = 0; i < this.inputArray.length; i++) {
  		if(value == this.inputArray[i]) {
  			this.inputArray.splice(i, 1);
  		}
  	}
  }
}

