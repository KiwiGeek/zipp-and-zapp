/// @description Insert description here
// You can write your code in this editor
Stars[NUM_OF_STARS] = [];
randomize();
for (var i = 0; i < NUM_OF_STARS; i++) 
{
	var _x = random_range(0, room_width);
	var _y = random_range(0, room_height);
	var _c = irandom_range(0, 255);
	Stars[i] = [_x, _y, _c];
}