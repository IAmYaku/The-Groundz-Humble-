//
//  SAUnitySuperAwesome.c
//  SuperAwesome
//
//  Created by Gabriel Coman on 13/05/2018.
//

#import <UIKit/UIKit.h>
#import "SAUnityCallback.h"
#import "AwesomeAds.h"

extern "C" {
    
    void SuperAwesomeUnityAwesomeAdsInit (bool loggingEnabled) {
        [AwesomeAds initSDK:loggingEnabled];
    }
    
    void SuperAwesomeUnityAwesomeAdsTriggerAgeCheck (const char *age) {
        
        NSString *ageString = [NSString stringWithUTF8String:age];
        
        [AwesomeAds triggerAgeCheck:ageString response:^(GetIsMinorModel *model) {
         
            GetIsMinorModel *finalModel = model != nil ? model : [[GetIsMinorModel alloc] init];
            NSDictionary *payload = [finalModel dictionaryRepresentation];
            if (payload != nil) {
                sendToUnity(@"AwesomeAds", payload);
            }
        }];
    }
}
