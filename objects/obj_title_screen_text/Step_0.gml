/// @description Re-calculate opacity, adjust the text, and advance as necessary
frame = frame + 1;

if (frame < 60) 
{
	// for the first second, fade the first page in
	opacity = min(opacity+0.017, 1.0)
}

if (frame >= 180 && frame < 240)
{
	// after three seconds, spend a second fading out
	opacity = max(opacity-0.017, 0.0)
}

// at the 240th frame, switch the text
if (frame == 240) { introText = "Present"; }

if (frame >=240 and frame < 300) 
{
	// for the fourth second, fade the second page in
	opacity = min(opacity+0.017, 1.0)
}

if (frame >= 360 && frame < 420)
{
	// after three seconds, spend a second fading out
	opacity = max(opacity-0.017, 0.0)
}

// at the 420th frame, make the screen visible
//if (frame = 420) { 	obj_logos.visible = true; }

if (frame >= 420 and frame < 480) 
{
	// for the fourth second, fade the second page in
	opacity = min(opacity+0.017, 1.0)
}

if (frame >= 660 && frame < 720)
{
	// after three seconds, spend a second fading out
	opacity = max(opacity-0.017, 0.0)
}

// the various ways we can move on to the main menu
if (frame == 720
	|| gamepad_button_check(0, gp_face1) 
	|| gamepad_button_check(0, gp_start)
	|| gamepad_button_check(0, gp_select)
	|| mouse_check_button(mb_left)
	|| keyboard_check(vk_anykey)) 
{ 
	room_goto(room_main_menu); 
}
	