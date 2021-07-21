/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import <UIKit/UIKit.h>
#import "SAUnityCallback.h"
#import "AwesomeAds.h"
#import "SASession.h"
#import "SuperAwesome.h"

#if defined(__has_include)
#if __has_include(<SuperAwesome/SuperAwesome-Swift.h>)
#import <SuperAwesome/SuperAwesome-Swift.h>
#else
#import "SuperAwesome-Swift.h"
#endif
#endif

extern "C" {
    
    /**
     * Native method called from Unity.
     * Add a callback to the SAVideoAd static class
     */
    void SuperAwesomeUnitySAVideoAdCreate () {
        [SAVideoAd setCallback:^(NSInteger placementId, SAEvent event) {
            switch (event) {
                case adLoaded: unitySendAdCallback(@"SAVideoAd", placementId, @"adLoaded"); break;
                case adEmpty: unitySendAdCallback(@"SAVideoAd", placementId, @"adEmpty"); break;
                case adFailedToLoad: unitySendAdCallback(@"SAVideoAd", placementId, @"adFailedToLoad"); break;
                case adAlreadyLoaded: unitySendAdCallback(@"SAVideoAd", placementId, @"adAlreadyLoaded"); break;
                case adShown: unitySendAdCallback(@"SAVideoAd", placementId, @"adShown"); break;
                case adFailedToShow: unitySendAdCallback(@"SAVideoAd", placementId, @"adFailedToShow"); break;
                case adClicked: unitySendAdCallback(@"SAVideoAd", placementId, @"adClicked"); break;
                case adEnded: unitySendAdCallback(@"SAVideoAd", placementId, @"adEnded"); break;
                case adClosed: unitySendAdCallback(@"SAVideoAd", placementId, @"adClosed"); break;
            }
        }];
    }
    
    /**
     * Native method called from Unity.
     * Load a video ad
     *
     * @param placementId   placement id
     * @param configuration production = 0 / staging = 1
     * @param test          true / false
     */
    void SuperAwesomeUnitySAVideoAdLoad(int placementId, int configuration, bool test, int playback) {
        [SAVideoAd setTestMode:test];
        [SAVideoAd setConfiguration:getConfigurationFromInt(configuration)];
        [SAVideoAd setPlaybackMode:getRTBStartDelayFromInt(playback)];
        [SAVideoAd load: placementId];
    }
    
    /**
     * Native method called from Unity.
     * Check to see if there is an video ad available
     *
     * @return true / false
     */
    bool SuperAwesomeUnitySAVideoAdHasAdAvailable(int placementId) {
        return [SAVideoAd hasAdAvailable: placementId];
    }
    
    /**
     * Native method called from Unity.
     * Play a video ad
     *
     * @param isParentalGateEnabled         true / false
     * @param isBumperPageEnabled           true / false
     * @param shouldShowCloseButton         true / false
     * @param shouldShowSmallClickButton    true / false
     * @param shouldAutomaticallyCloseAtEnd true / false
     * @param orientation                   ANY = 0 / PORTRAIT = 1 / LANDSCAPE = 2
     */
    void SuperAwesomeUnitySAVideoAdPlay(int placementId, bool isParentalGateEnabled, bool isBumperPageEnabled, bool shouldShowCloseButton, bool shouldShowSmallClickButton, bool shouldAutomaticallyCloseAtEnd, int orientation) {
        UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
        [SAVideoAd setParentalGate:isParentalGateEnabled];
        [SAVideoAd setBumperPage:isBumperPageEnabled];
        [SAVideoAd setCloseButton:shouldShowCloseButton];
        [SAVideoAd setSmallClick:shouldShowSmallClickButton];
        [SAVideoAd setCloseAtEnd:shouldAutomaticallyCloseAtEnd];
        [SAVideoAd setOrientation: getOrientationFromInt (orientation)];
        [SAVideoAd play: placementId fromVC: root];
    }
    
}
