/// @description Insert description here
// You can write your code in this editor
for (var i = 0; i < NUM_OF_STARS; i++) 
{
	var _x = Stars[i][0]
	var _c = Stars[i][2]
	
	_x = _x - (_c / 255);
	
	if (_x < 0) 
	{
		_x = room_width;
		_y = random_range(0, room_height);
		Stars[i][1] = _y;
	}
	Stars[i][0] = _x;
}