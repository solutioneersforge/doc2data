import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  collapseNavbar() {
    const navbarCollapse = document.getElementById('navbarNav');
    if (navbarCollapse) {
      navbarCollapse.classList.remove('show');
    }
  }
}
