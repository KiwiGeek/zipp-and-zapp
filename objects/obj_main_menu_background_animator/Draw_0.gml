/// @description Insert description here
// You can write your code in this editor
draw_clear(c_black)
for (i = 0; i < NUM_OF_STARS; i++) 
{
	var _x = Stars[i][0];
	var _y = Stars[i][1];
	var _c = Stars[i][2];
	draw_point_color(_x, _y, make_color_rgb(_c, _c, _c));
	if (_c > 235) 
	{
		draw_point_color(_x - 1, _y, make_color_rgb(_c/2, _c/2, _c/2));
		draw_point_color(_x + 1, _y, make_color_rgb(_c/2, _c/2, _c/2));
		draw_point_color(_x, _y - 1, make_color_rgb(_c/2, _c/2, _c/2));
		draw_point_color(_x, _y + 1, make_color_rgb(_c/2, _c/2, _c/2));
	}
}