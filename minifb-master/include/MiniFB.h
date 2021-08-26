#ifndef _MINIFB_H_
#define _MINIFB_H_

#include "MiniFB_enums.h"

#ifdef __cplusplus
extern "C" {
#endif

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#define MFB_RGB(r, g, b)    (((uint32_t) r) << 16) | (((uint32_t) g) << 8) | ((uint32_t) b)

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Create a window that is used to display the buffer sent into the mfb_update function, returns 0 if fails
__declspec(dllexport) struct mfb_window * mfb_open(const char *title, unsigned width, unsigned height);
__declspec(dllexport) struct mfb_window * mfb_open_ex(const char *title, unsigned width, unsigned height, unsigned flags);

// Update the display
// Input buffer is assumed to be a 32-bit buffer of the size given in the open call
// Will return a negative status if something went wrong or the user want to exit
// Also updates the window events
__declspec(dllexport) mfb_update_state    mfb_update(struct mfb_window *window, void *buffer);

__declspec(dllexport) mfb_update_state    mfb_update_ex(struct mfb_window *window, void *buffer, unsigned width, unsigned height);

// Only updates the window events
__declspec(dllexport) mfb_update_state    mfb_update_events(struct mfb_window *window);

// Close the window
__declspec(dllexport) void                mfb_close(struct mfb_window *window);

// Set user data
__declspec(dllexport) void                mfb_set_user_data(struct mfb_window *window, void *user_data);
__declspec(dllexport) void *              mfb_get_user_data(struct mfb_window *window);

// Set viewport (useful when resize)
__declspec(dllexport) bool                mfb_set_viewport(struct mfb_window *window, unsigned offset_x, unsigned offset_y, unsigned width, unsigned height);

// DPI
// [Deprecated]: Probably a better name will be mfb_get_monitor_scale
__declspec(dllexport) void                mfb_get_monitor_dpi(struct mfb_window *window, float *dpi_x, float *dpi_y);
// Use this instead
__declspec(dllexport) void                mfb_get_monitor_scale(struct mfb_window *window, float *scale_x, float *scale_y);

// Callbacks
__declspec(dllexport) void                mfb_set_active_callback(struct mfb_window *window, mfb_active_func callback);
__declspec(dllexport) void                mfb_set_resize_callback(struct mfb_window *window, mfb_resize_func callback);
__declspec(dllexport) void                mfb_set_keyboard_callback(struct mfb_window *window, mfb_keyboard_func callback);
__declspec(dllexport) void                mfb_set_char_input_callback(struct mfb_window *window, mfb_char_input_func callback);
__declspec(dllexport) void                mfb_set_mouse_button_callback(struct mfb_window *window, mfb_mouse_button_func callback);
__declspec(dllexport) void                mfb_set_mouse_move_callback(struct mfb_window *window, mfb_mouse_move_func callback);
__declspec(dllexport) void                mfb_set_mouse_scroll_callback(struct mfb_window *window, mfb_mouse_scroll_func callback);

// Getters
__declspec(dllexport) const char *        mfb_get_key_name(mfb_key key);

__declspec(dllexport) bool                mfb_is_window_active(struct mfb_window *window);
__declspec(dllexport) unsigned            mfb_get_window_width(struct mfb_window *window);
__declspec(dllexport) unsigned            mfb_get_window_height(struct mfb_window *window);
__declspec(dllexport) int                 mfb_get_mouse_x(struct mfb_window *window);             // Last mouse pos X
__declspec(dllexport) int                 mfb_get_mouse_y(struct mfb_window *window);             // Last mouse pos Y
__declspec(dllexport) float               mfb_get_mouse_scroll_x(struct mfb_window *window);      // Mouse wheel X as a sum. When you call this function it resets.
__declspec(dllexport) float               mfb_get_mouse_scroll_y(struct mfb_window *window);      // Mouse wheel Y as a sum. When you call this function it resets.
__declspec(dllexport) const uint8_t *     mfb_get_mouse_button_buffer(struct mfb_window *window); // One byte for every button. Press (1), Release 0. (up to 8 buttons)
__declspec(dllexport) const uint8_t *     mfb_get_key_buffer(struct mfb_window *window);          // One byte for every key. Press (1), Release 0.

// FPS
__declspec(dllexport) void                mfb_set_target_fps(uint32_t fps);
__declspec(dllexport) unsigned            mfb_get_target_fps();
__declspec(dllexport) bool                mfb_wait_sync(struct mfb_window *window);

// Timer
__declspec(dllexport) struct mfb_timer *  mfb_timer_create(void);
__declspec(dllexport) void                mfb_timer_destroy(struct mfb_timer *tmr);
__declspec(dllexport) void                mfb_timer_reset(struct mfb_timer *tmr);
__declspec(dllexport) double              mfb_timer_now(struct mfb_timer *tmr);
__declspec(dllexport) double              mfb_timer_delta(struct mfb_timer *tmr);
__declspec(dllexport) double              mfb_timer_get_frequency(void);
__declspec(dllexport) double              mfb_timer_get_resolution(void);

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#ifdef __cplusplus
}

#include "MiniFB_cpp.h"
#endif

#endif
