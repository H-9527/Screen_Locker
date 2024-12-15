# Screen_Locker
This repository contains a simple C# Windows Forms application that functions as a screen locker. The application forces the user to take a break by displaying a full-screen lock with a countdown timer. Once the timer finishes, the screen unlocks automatically or can be manually closed using a provided button.
## Disclaimer
**This application is intended for educational purposes to demonstrate a simple C# implementation of a screen locker. Use responsibly and ensure compliance with relevant policies and permissions.**
## Features
- **Full-Screen Lock:** The application maximizes to cover the entire screen and removes the border, disabling user interaction with other applications.
<img src="https://github.com/H-9527/Screen_Locker/blob/main/images/locker.png" alt="locker" width="400"/>

- **Countdown Timer:** A countdown timer is displayed, reminding the user of the remaining time until the lock screen can be closed.
- **Forced Rest Reminder:** The message "GO REST!" is prominently displayed to encourage the user to take a break.
- **Auto Close:** Automatically closes the lock screen after the countdown reaches zero.
- **Manual Close Button:** A "Close" button becomes available after the timer finishes, allowing manual dismissal.
<img src="https://github.com/H-9527/Screen_Locker/blob/main/images/close_tab.png" alt="close_tab" width="400"/>

- **Prevent Premature Closing:** The application prevents the user from closing the window before the countdown ends.
## Code Highlights
- **Countdown Logic:**  
```C#
// Timer event handler
private void Timer_Tick(object sender, EventArgs e)
{
    remainingTime--; // Decrease the remaining time by 1 second

    if (remainingTime >= 0)
    {
        counterLabel.Text = $"Remaining Time: {remainingTime} seconds";
    }
    else
    {
        counterLabel.Text = "Time's up!";
        timer.Stop();
        // Additional actions for closing functionality...
    }
}
```
- **Automatic Closure:**
```C#
// Automatically close the form after a delay
Task.Delay(10000).ContinueWith(_ =>
{
    if (!this.IsDisposed)
    {
        this.Invoke(new Action(() =>
        {
            this.Close();
        }));
    }
});
```
- **User Restriction:**
```C#
// Prevent premature closure
protected override void OnFormClosing(FormClosingEventArgs e)
{
    if (e.CloseReason == CloseReason.UserClosing && !counterDown)
    {
        e.Cancel = true; // Cancel the closing operation
    }

    base.OnFormClosing(e);
}
```

## Future Improvements
- **Password Protection:** Add an option to require a password to unlock.
- **Customizable Timer:** Allow users to set their desired countdown duration.
- **Break Tracking:** Log user break activity for health tracking purposes.
  
