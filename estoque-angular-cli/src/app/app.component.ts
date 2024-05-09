import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ManagerComponent } from './manager/manager.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ManagerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'estoque-angular-cli';
}
