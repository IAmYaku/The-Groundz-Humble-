/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import <UIKit/UIKit.h>
#import "SAUnityCallback.h"
#import "AwesomeAds.h"
#import "SASession.h"

extern "C" {
    
    /**
     * Native method called from Unity.
     * Method that adds a callback to the SAInterstitialAd static method class
     */
    void SuperAwesomeUnitySAInterstitialAdCreate () {
        [SAInterstitialAd setCallback:^(NSInteger placementId, SAEvent event) {
            switch (event) {
                case adLoaded: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adLoaded"); break;
                case adEmpty: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adEmpty"); break;
                case adFailedToLoad: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adFailedToLoad"); break;
                case adAlreadyLoaded: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adAlreadyLoaded"); break;
                case adShown: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adShown"); break;
                case adFailedToShow: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adFailedToShow"); break;
                case adClicked: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adClicked"); break;
                case adEnded: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adEnded"); break;
                case adClosed: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adClosed"); break;
            }
        }];
        
    }
    
    /**
     * Native method called from Unity.
     * Load an interstitial ad
     *
     * @param placementId   the placement id to try to load an ad for
     * @param configuration production = 0 / staging = 1
     * @param test          true / false
     */
    void SuperAwesomeUnitySAInterstitialAdLoad (int placementId, int configuration, bool test) {
        [SAInterstitialAd setTestMode:test];
        [SAInterstitialAd setConfiguration:getConfigurationFromInt(configuration)];
        [SAInterstitialAd load: placementId];
    }
    
    /**
     * Native method called from Unity.
     * Check to see if there's an ad available
     *
     * @return true / false
     */
    bool SuperAwesomeUnitySAInterstitialAdHasAdAvailable(int placementId) {
        return [SAInterstitialAd hasAdAvailable: placementId];
    }
    
    /**
     * Native method called from Unity.
     * Play an interstitial ad
     *
     * @param isParentalGateEnabled true / false
     * @param isBumperPageEnabled   true / false
     * @param orientation           ANY = 0 / PORTRAIT = 1 / LANDSCAPE = 2
     */
    void SuperAwesomeUnitySAInterstitialAdPlay (int placementId, bool isParentalGateEnabled, bool isBumperPageEnabled, int orientation) {
        UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
        [SAInterstitialAd setParentalGate:isParentalGateEnabled];
        [SAInterstitialAd setBumperPage:isBumperPageEnabled];
        [SAInterstitialAd setOrientation:getOrientationFromInt (orientation)];
        [SAInterstitialAd play: placementId fromVC: root];
    }
    
}
