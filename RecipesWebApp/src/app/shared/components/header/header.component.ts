import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'primeng/tabs';
import { Routes } from '@app/core/enums/routes.enum';
import { RouterLink } from '@angular/router';

interface MenuItem {
  route: string;
  label: string;
  icon: string;
}

@Component({
  selector: 'app-header',
  imports: [CommonModule, TabsModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent implements OnInit {
  items: MenuItem[] = [];

  ngOnInit() {
    this.items = [
      { label: 'Home', icon: 'fa-solid fa-house', route: Routes.Home },
      {
        label: 'Ingredients',
        icon: 'fa-solid fa-kitchen-set',
        route: Routes.Ingredients,
      },
      { label: 'Ners', icon: 'fa-solid fa-carrot', route: Routes.Ners },
      {
        label: 'Directions',
        icon: 'fa-solid fa-person-chalkboard',
        route: Routes.Directions,
      },
      { label: 'Recipes', icon: 'fa-solid fa-book', route: Routes.Recipes },
    ];
  }
}
