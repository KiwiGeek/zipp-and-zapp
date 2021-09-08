/// @description Insert description here
// You can write your code in this editor
if ((keyboard_check(vk_up) && !up_was_pressed) || (gamepad_button_check(0,gp_padu) && !gamepad_up_was_pressed))
{
	selected_item--;
	if (selected_item == -1) { selected_item = 3; }
}

if ((keyboard_check(vk_down) && !down_was_pressed) || (gamepad_button_check(0,gp_padd) && !gamepad_down_was_pressed))
{
	selected_item++;
	if (selected_item == 4) { selected_item = 0; }
}

up_was_pressed = keyboard_check(vk_up);
down_was_pressed = keyboard_check(vk_down);
gamepad_up_was_pressed = gamepad_button_check(0,gp_padu);
gamepad_down_was_pressed = gamepad_button_check(0,gp_padd);