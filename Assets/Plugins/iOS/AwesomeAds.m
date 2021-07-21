
#import "AwesomeAds.h"
#import "SAEvents.h"
#import "SAFileDownloader.h"

static BOOL isInitialised = false;

@implementation AwesomeAds

+ (void) initSDK: (BOOL) loggingEnabled {
    if (!isInitialised) {
        NSLog(@"Initialising AwesomeAds");
        [SAEvents initMoat: loggingEnabled];
        [SAFileDownloader cleanup];
        isInitialised = true;
    } else {
        NSLog(@"Already initialised AwesomeAds");
    }
}

+ (void) triggerAgeCheck: (NSString*) age response:(GetIsMinorBlock)response {
    [[SAAgeCheck sdk] getIsMinor:age :response];
}

@end
