/// @description Display text or logo
if (frame < 420) 
{
	draw_set_alpha(opacity);
	draw_text(room_width/2, room_height /2, introText);
}
else 
{
	draw_sprite_ext(spr_title_screen, title_screen_to_draw, 0, 0, 1, 1, 0, c_white, opacity);
}