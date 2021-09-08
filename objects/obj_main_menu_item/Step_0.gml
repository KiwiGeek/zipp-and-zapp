//if (collision_point(mouse_x, mouse_y, id, true, false)
if ((instance_position(mouse_x, mouse_y, self) && (mouse_x != last_mouse_x || mouse_y != last_mouse_y))
	|| obj_main_menu_keyboard_controller.selected_item == self.menu_position)
{	
	obj_main_menu_keyboard_controller.selected_item = self.menu_position;
	multiplier = min(multiplier+0.03, 0.999);
}
else 
{
	multiplier = max(multiplier-0.03, 0.7);
}

image_alpha = multiplier;
image_xscale = multiplier;
image_yscale = multiplier;
last_mouse_x = mouse_x;
last_mouse_y = mouse_y;