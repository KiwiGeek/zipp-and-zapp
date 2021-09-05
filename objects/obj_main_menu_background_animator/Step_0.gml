/// @description Insert description here
// You can write your code in this editor
background_y_offset = (sin(frame/60) - 0.5) * 20
background_x_offset -= 1;
layer_x(bgid, background_x_offset);
layer_y(bgid, background_y_offset);
frame++;