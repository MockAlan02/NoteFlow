export interface MenuItem {
  label: string; 
  icon?: string; // Optional if you using icon
  route?: string; //For Navigation
  children?: MenuItem[]; //For submenus
}
