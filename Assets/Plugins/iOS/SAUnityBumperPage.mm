/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import <UIKit/UIKit.h>
#import "SABumperPage.h"

extern "C" {
    
    /**
     * Unity to native iOS method that overrides the current version & sdk
     * strings so that this will get reported correctly in the dashboard.
     *
     * @param nameString pointer to an array of chars containing the version
     */
    void SuperAwesomeUnityBumperOverrideName (const char *nameString) {
        
        NSString *name = [NSString stringWithUTF8String:nameString];
        [SABumperPage overrideName: name];
    }
}
