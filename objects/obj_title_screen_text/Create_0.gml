/// @description Sets up the text renderer
randomize();
introText = " - The Penmans - "; // The first text we're going to print
frame = 0;	// Our local frame counter, so that we can sorta animate the fade ins/outs
opacity = 0.0;	// how much alpha we're going to draw everything with
title_screen_to_draw = floor(random_range(0,3));	// choose the logo we want
draw_set_font(fnt_title_screen);	// setup the font
draw_set_halign(fa_center);			// make draw_text be origined from the center
draw_set_valign(fa_middle);
draw_set_color(c_white);			// draw in white
audio_play_sound(snd_title_screen, 100, true);