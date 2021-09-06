//if (collision_point(mouse_x, mouse_y, id, true, false)
if (instance_position(mouse_x, mouse_y, self))
{	
	multiplier = min(multiplier+0.03, 0.999);
}
else 
{
	multiplier = max(multiplier-0.03, 0.7);
}

image_alpha = multiplier;
image_xscale = multiplier;
image_yscale = multiplier;